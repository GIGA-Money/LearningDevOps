import { BlogActions } from "./action.interface";

@Injectable()
export class BlogEffects {
    loadBlogs$ = createEffect(() => this.actions$.pipe(ofType('[Blog Page] Load Blog'),
        mergeMap(() => this.blogService.getAll().pipe(
            map(blogs => ({
                type: '[Blogs API] Blogs Loaded Success', payload: blogs
            })),
            catchError(() => EMPTY)
        ))
    )
);
    constructor(private actions$: BlogActions, private blogService: BlogService) { }

}
/**
 * the above will create a new effect named loadBlogs$. this effect is responsible for calling the getAll() method 
 * from the blog service to retrieve the list oof blogs from teh external endpoint,
 * while it is also responsible for dispatching the new action that passes the retrieved blog list into the reducer to modify the store.
 */