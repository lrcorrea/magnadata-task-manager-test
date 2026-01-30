<script setup lang="ts">
import type { Task } from "~/types/task";
import TaskItem from "./TaskItem.vue";

defineProps<{
  tasks: Task[];
}>();

const emit = defineEmits<{
  (e: "complete", id: string): void;
}>();
</script>

<template>
  <ul class="tasks-list">
    <TaskItem
      v-for="task in tasks"
      :key="task.id"
      :task="task"
      @complete="emit('complete', $event)"
    />
  </ul>
</template>
