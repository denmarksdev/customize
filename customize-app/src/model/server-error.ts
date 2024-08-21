export interface ServerError {
    errors: Map<string, string>,
    success: boolean,
    message: string
}
