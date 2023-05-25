import { FormGroup, FormControl } from '@angular/froms';

export class FoodComponent {
    /*
    Grouping from controls
    Form Group - creats a from wit ha fixed set of from controls
    Form Array - creatsa a fro mwit hdynamic from controls.
    */

    foodForm = new FormGroup({
        firstName: new FormControl(''),
        lastName: new FormControl(''),
        knownAs: new FormControl(''),
        address: new FormControl({
            street: new FormControl(''),
            city: new FormControl(''),
            country: new FormControl('')
            /*
            in a online web front for a restaurant, would use county over country, 
            to indicate of which store would be most appropiate to use.
            */
        })
    });

    onSubmit() {
        //  display the value of form group in the form of JSON
        console.warn(this.foodForm.value);
    }

}