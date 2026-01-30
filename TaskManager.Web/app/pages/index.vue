<script setup lang="ts">
import { useTasks } from "~/composables/useTasks";
import { taskService } from "~/services/taskService";
import TaskList from "~/components/Task/TaskList.vue";
import { TaskFilter, TaskFilterOption } from "~/types/taskFilter";

useSeoMeta({
  title: "Gerenciador de Tarefas",
  description:
    "Aplicação de gerenciamento de tarefas composta por uma API em ASP.NET Core e um frontend em Nuxt 3.",
});

const { tasks, loading, error, loadTasks } = useTasks();

const description = ref("");
const creating = ref(false);
const taskFilter = ref("");
const taskDescriptionError = ref(false);

const taskFilterOptions: TaskFilterOption[] = [
  {
    value: "all",
    label: "Todas",
  },
  {
    value: "pending",
    label: "Pendentes",
  },
  {
    value: "completed",
    label: "Concluídas",
  },
];

const createTask = async () => {
  if (!description.value.trim()) {
    taskDescriptionError.value = true;
    return;
  }

  creating.value = true;
  try {
    await taskService.create(description.value);
    taskDescriptionError.value = false;
    description.value = "";
    await loadTasks();
  } finally {
    creating.value = false;
  }
};

const completeTask = async (id: string) => {
  await taskService.complete(id);
  console.log('taskFilter', taskFilter.value);
  await loadTasks(taskFilter.value, false);
};

watch(taskFilter, (value) => {
  loadTasks(value);
});

watch(description, (newValue) => {
  if (newValue.trim()) {
    taskDescriptionError.value = false;
  }
});

onMounted(loadTasks);
</script>

<template>
  <div class="wrapper">
    <main class="container">
      <h1 class="title">Gerenciador de Tarefas</h1>

      <div class="container-add-task-form">
        <form @submit.prevent="createTask" class="add-task-form">
          <input
            v-model="description"
            placeholder="Descreva sua tarefa"
            class="add-task-input"
            :class="{ 'input-error': taskDescriptionError }"
          />
          <button
            :disabled="creating"
            title="Adicionar Tarefa"
            class="add-task-button"
          >
            +
          </button>
        </form>

        <select v-model="taskFilter" class="task-filter-select">
          <option disabled value="">Filtrar</option>
          <option
            v-for="option in taskFilterOptions"
            :key="option.value"
            :value="option.value"
          >
            {{ option.label }}
          </option>
        </select>
      </div>

      <section class="container-tasks">
        <TaskList :tasks="tasks" @complete="completeTask" />

        <div class="tasks-loading" v-if="loading">
          <svg width='40px' height='40px' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid" class="uil-ring-alt">
            <rect x="0" y="0" width="100" height="100" fill="none" class="bk"></rect>
            <circle cx="50" cy="50" r="40" stroke="#eee" fill="none" stroke-width="10" stroke-linecap="round"></circle>
            <circle cx="50" cy="50" r="40" stroke="#6c63ff" fill="none" stroke-width="10" stroke-linecap="round">
                <animate attributeName="stroke-dashoffset" dur="1.8s" repeatCount="indefinite" from="0" to="502"></animate>
                <animate attributeName="stroke-dasharray" dur="1.8s" repeatCount="indefinite" values="150.6 100.4;1 250;150.6 100.4"></animate>
            </circle>
        </svg>
        </div>

        <div class="empty-list" v-if="!loading && tasks.length === 0">
          Nenhuma tarefa encontrada.
        </div>
      </section>
    </main>
  </div>
</template>
