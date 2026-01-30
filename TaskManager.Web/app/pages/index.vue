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
  if (!description.value.trim()) return;

  creating.value = true;
  try {
    await taskService.create(description.value);
    description.value = "";
    await loadTasks();
  } finally {
    creating.value = false;
  }
};

const completeTask = async (id: string) => {
  await taskService.complete(id);
  await loadTasks(taskFilter);
};

watch(taskFilter, (value) => {
  loadTasks(value);
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
      </section>
    </main>
  </div>
</template>
