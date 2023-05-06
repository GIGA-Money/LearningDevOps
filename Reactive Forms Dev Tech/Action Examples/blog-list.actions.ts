import { createAction, props } from '@ngrx/store';

export const setBlogList = createAction(
    '[Blog] Set Blog List',
    props<{ blogs: ReadonlyArray<Blog> }>());

ngOnInit(): void{
    store.dispatch(setBlogList({blogs: this.blogs}));
}