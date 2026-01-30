export interface Task {
  id: number;
  description: string;
  createdAt: string;
  completedAt: string | null;
  isCompleted: boolean;
}
