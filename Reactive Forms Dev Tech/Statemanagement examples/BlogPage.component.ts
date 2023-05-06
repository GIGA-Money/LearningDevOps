/* effects are specific elemetns that hte NgRx library uses.
 this is an RxJs-powered side effect model that handles external interactions such as API calls, web sokete messages, and time-based events.
 using effects, we can isolate our compounets from interacting with external sources and reduce their responsiblilties.
*/
// application without side effects
export class BlogPageComponent implements OnInit {
    movies: Blog[];
    blogs: any;
    constructor(private blogService: MoviServices) { }

    ngOnInit(): void {
        this.blogService
        .getAll()
        .subscribe(blogs => this.blogs = blogs);
    }
}
/**
 * with the above, we have a component that has several responsibliites
 * managing the satte of the blogs
 * using the blog service to call an external API to get the list of blogs
 * modifying the state of the blog inside the component
 */

// Application with side effects
export class BlogsPageComponent implements OnInit{
    blogs$: Observable<Blog[]> = this.store.select(state => state.blog);
    constructor(private store: Store<{blogs: Blog[]}>){}
    ngOnInit(): void {
        this.store.dispatch({type: '[Blog Page] Load Blog'});
    }
}

/**
 * the above - the code fore the blog page component's responsilbity will now dispatch 
 * an action that will allow effets to identify what service needs to be called to retrieve the data.
 */