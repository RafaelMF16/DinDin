export interface ProblemDetailsError {
    title: string;
    detail: string;
    errors?: Record<string, string[]>;
    status?: number;
    type?: string;
    instance?: string;
}