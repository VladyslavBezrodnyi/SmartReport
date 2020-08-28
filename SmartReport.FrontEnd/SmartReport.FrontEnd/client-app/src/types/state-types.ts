import { AppUser } from "./common-types";
import { UserDTO, TaskDTO, VisitDateDTO, ReportDTO } from "./DTO-types";

export interface AuthState {
    isAuthenticated: boolean;
    work?: VisitDateDTO| null;
    user:AppUser | null;
}

export interface AccountState {
    usersList: UserDTO[]
}

export interface TaskState {
    tasksList: TaskDTO[],
    userTasksList: TaskDTO[],
}

export interface ReportState {
    userReportsList: ReportDTO[]
}