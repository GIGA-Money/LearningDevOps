// strict state immutablility:
// violation example
export const reducer = createReducer(initialState, on(addBlog, (state, { blog }) => (
    {
        // violation 1: assign ta new value to loading
        state.loading = false,
        // viloation 2: 'push' modifies the array
        // state.blogs.push(blog)
    }
)));

// the fix for the above ^
export const reducer = createReducer(
    initialState, on(addBlog, (state, { blog })
        =>
    ({
        // fix return the state as a whole object with the new values
        ...state,
        loading: false,
        blogs: [...state.blogs, blog]
    }))
);

// Strict action immutablity - a runtime check to verify if the dev modifies teh action
// example violation
export const reducer = createReducer(intialState, on(addBlog,
    (state, { blog }) =>
        // violation not allowed to modify an action
        blog.id = uniqueID();
return {
    ...state, blogs: [...state.blogs, blog]
}));

//  violation fix example
export const addBlog = createAction(
    '[Blog List] Add Blog',
    // fix: return teh object in teh action with the new value
    (descriptiion: string) =>
        ({ id: uniqueID(), descriptiion })
);
// concludes in blog.reducer.ts

// Strict State Serializability
//  check verifies if the values placed in the state are serializble. essentiall for persisting the state so that it can be rehydrated in teh future
export const reducer = createReducer(
    initialState,
    on(addBlog, (state, { blog }) =>
    ({
        ...state,
        blogs: [...state.blogs, blog],
        // violation: data type is not a serializable value.
        createdOn: new Date()
    }))
);

// the fix
export const reducer = createReducer(
    initialState,
    on(addBlog, (state, { blog })
        => ({
            ...state,
            blogs: [...state.blogs, blog],
            // fix: should convert the date into a JSON object.
            createdOn: new Date().toJSON()
        }))
);


// strict Action Serializability
// runtime check similar to the strict state serializability, but for actions, checks if the sates are serializable, done by the redux dev tools to debug errors.
const createBlog = creatAction(
    '[Blog List] Add Bog',
    blog => ({
        blog,
        // violation, a function is not serializable
        logBlog: () => { console.log(blog); },
    })
);

// fix
const createBlog = createAction(
    '[Blog List] Add Blog',
    // should use props to receive paramters
    props<{ blog: Blog }>()
);

//  strict Action Within NgZone
//  a runtime chekc to verify if the actions are dispatched by asyn task with NGZone, check i deactivated by default
// callback outsie NGZone
// violation: createBlog actions is invoked outside the NgZone
callbackOutsideNgZone(){
    this.store.dispatch(createBlog());
}

// fix
import { NgZone } from '@angulare/core';
constructor(private ngZone: NgZone){ }
//  use run() function to call the dispatch inside the NgZone
function callbackOutsideNgZone() {
    this.ngZone.run(
        () => {
            this.store.dispatch(createBlog());
        }
    );
}

// strict action type uniquness
// runtime check prevents developers from registering the same action type more than once, check is deactivated by default
// vilation: to actions have the same type
export const addBlog = creatAction('[Blog] Add Blog');
export modifyBlog = creatAction('[Blog] Add Blog');

// fix
export const addBlog = creatAction('[Blog] Add Blog');
export modifyBlog = creatAction('[Blog] Modify Blog');

