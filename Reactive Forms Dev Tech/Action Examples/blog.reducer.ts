export const reducer = createReducer(
    initialState,
    on(addBlog, (State, {blog}) =>
    ({
        ...State,
        blogs: [...State.blogs, blog]
    }))
);

