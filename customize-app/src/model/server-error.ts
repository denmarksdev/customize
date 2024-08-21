export interface ServerError {
    errors: Map<string, string>,
    success: boolean,
    message: string
}

export interface ServerResponse<T> extends ServerError 
{
    data:T
}
