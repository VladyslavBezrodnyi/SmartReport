export interface LoginDTO {
    email: string;
    password: string;
}

export interface RegistrationDTO {
    name: string;
    email: string;
    password: string;
}

export interface UserDTO {
    id: string;
    userName: string;
    name: string;
    position: string;
    isWork: boolean;
}

export interface TaskDTO {
    id?: number;
    name?: string;
    description?: string;
    startDate?: Date;
    deadLine?: Date
    place?: PlaceDTO
}

export interface PlaceDTO {
    id?: number;
    Name?: string;
    Description?: string;
}

export interface UserTaskDTO {
    taskId: number;
    userId: string;
}

export interface ReportDTO {
    id?: number;
    reportText: string;
    date: Date;
    tasks: TaskDTO[];
}

export interface VisitDateDTO {
    userId: string;
    isWork: boolean;
    workTime: any;
}