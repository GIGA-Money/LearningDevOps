import { createAction, props } from "@ngrx/store";

export enum AuthActions{
    LOGIN = '[AUTH] Login',
    SET_TOKEN = '[AUTH] Set Token',
    CREATE_USER = '[AUTH] Create User',
    AUTH_ERROR = '[AUTH] AUTH_ERROR',
}
export const setToken = createAction(
    AuthActions.SET_TOKEN,
    props<{token: string}>()
)
export const setError = createAction(
    AuthActions.AUTH_ERROR,
    props<{error: any}>()
)

