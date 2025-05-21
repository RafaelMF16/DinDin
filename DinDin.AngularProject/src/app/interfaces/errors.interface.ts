export interface Errors {
    title: string;
    detail: string;
    errors?: Record<string, string[]>;
}