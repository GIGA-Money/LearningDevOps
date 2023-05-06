import { creatSelector, creatFeatureSelector } from '@ngrx/store';

export const selectBlogState = createFeatureSelector<BlogState>('blogState');

export const selectBlogs = () => createSelector (
    selectBlogState,
    (state: BlogsState) => state.blogs
)

export const selectBlogs =  (state: AppState) => state.blogs

blogs$ = this.store.select(selectBlogs())
constructor(private store: Store<AppState>){
    this.blogs$.subscribe((data) => {
        this.blogs = data;
    });
}

