import type { Task } from "~/types/task";
import { taskService } from "~/services/taskService";
import type { TaskFilter } from "~/types/filter";

export function useTasks() {
  const tasks = ref<Task[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);

  const loadTasks = async (filter?: TaskFilter) => {
    loading.value = true;
    error.value = null;

    try {
      tasks.value = await taskService.getAll(filter);
    } catch {
      error.value = "Failed to load tasks";
    } finally {
      loading.value = false;
    }
  };

  return {
    tasks,
    loading,
    error,
    loadTasks,
  };
}
