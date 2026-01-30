import type { TaskFilter } from "~/types/filter";
import type { Task } from "~/types/task";
import { useApi } from "~/utils/api";

interface ApiResponse<T> {
  data: T;
}

export const taskService = {
  async getAll(filter?: TaskFilter): Promise<Task[]> {
    const api = useApi();
    const response = await api<ApiResponse<Task[]>>("/tasks", {
      params:
        filter && filter !== "all"
          ? { completed: filter == "completed" }
          : undefined,
    });
    return response.data;
  },

  async create(description: string): Promise<Task> {
    const api = useApi();
    const response = await api<ApiResponse<Task>>("/tasks", {
      method: "POST",
      body: { description },
    });
    return response.data;
  },

  async complete(id: string): Promise<void> {
    const api = useApi();
    await api(`/tasks/${id}/complete`, {
      method: "PATCH",
    });
  },
};
