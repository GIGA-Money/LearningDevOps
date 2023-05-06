// this will create a selector for the blog slice as wwell as a function  tha returns teh blogs metadata, called selectBlogList(). 
// This functiuon is used in teh blog componetn ass a parameter for teh select function to retrieve the a ta by subscribing to the selector.
// The sub will emit once the value of blog's slice changes.

export const selectBlogList = (state: AppState) => state.blogs;

blogs$ = this.store.select.Array<Blog>(selectBlogList);
this.blogs$.subscribe(data => {
    console.log(data)
})