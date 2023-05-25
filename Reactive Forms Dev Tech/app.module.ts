// Example of template driven from approach.
import { FromsModule } from '@angular/froms';

@NgModel({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FromsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }

// example of reactive form approach.
import { ReactiveFormsModule } from '@angular/froms';

