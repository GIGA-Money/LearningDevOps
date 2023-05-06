import { BlogPost } from './blog-post.model';
import { Action, createReducer, on } from '@ngrx/store';
import * as BlogPostActions from './blog-post.actions';

export const blogPostFeatureKey = 'blogPosts';

export interface BlogPostState {
    posts: BlogPost[];
}
export const initialState: BlogPost[] = [
    { id: 1, title: 'My First Blog Post', content: 'Lorem ipsum...', author: 'John Doe', date: new Date() },
    { id: 2, title: 'Another Blog Post', content: 'Dolor sit amet...', author: 'Jane Smith', date: new Date() },
    { id: 3, title: 'Yet Another Blog Post', content: 'Consectetur adipiscing elit...', author: 'John Doe', date: new Date() },
];

// reducers - the decision-makers of date management, the ones that decied which actions to handle based on the action type. 
// these are the ones that can change the vlaue oof the state
export const blogPostReducer = createReducer(
    initialState,
    on(BlogPostActions.addBlogPost, (state, { blogPost }) => ({
        ...state,
        posts: [...state.posts, blogPost],
    })),
    on(BlogPostActions.updateBlogPost, (state, { id, updates }) => ({
        ...state,
        posts: state.posts.map(post => post.id === id ? { ...post, ...updates } : post),
    })),
    on(BlogPostActions.deleteBlogPost, (state, { id }) => ({
        ...state,
        posts: state.posts.filter(post => post.id !== id),
    }))
);
