<script setup lang="ts">
import type { Task } from "~/types/task";

defineProps<{
  task: Task;
}>();

const emit = defineEmits<{
  (e: "complete", id: string): void;
}>();

const completeTask = (task: Task) => {
  emit("complete", task.id);
};
</script>

<template>
  <li class="task-item">
    <button
      title="Completar Tarefa"
      :disabled="task.completedAt"
      @click="completeTask(task)"
      class="btn-complete-task"
      :class="{ completed: task.completedAt }"
    >
      <svg
        fill="#000000"
        width="800px"
        height="800px"
        viewBox="0 0 1920 1920"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M1827.701 303.065 698.835 1431.801 92.299 825.266 0 917.564 698.835 1616.4 1919.869 395.234z"
          fill-rule="evenodd"
        />
      </svg>
    </button>

    <div class="description">
      <div class="text">{{ task.description }}</div>

      <small class="dates">
        <template v-if="task.completedAt">
          {{ formatDate(task.createdAt) }} – {{ formatDate(task.completedAt) }}
        </template>
        <template v-else>
          {{ formatDate(task.createdAt) }}
        </template>
      </small>
    </div>
  </li>
</template>
