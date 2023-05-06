import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/form';

@Component({
    selector: 'order-entry',
    templateUrl: './order-entry.component.html',
    styleUrls: ['./order-entry.component.sass'],
})
export class OrderEntry implements OnInit {

    foodForm = this.fb.group({
        firstName: new FormControl(''),
        lastName: new FormControl(''),
        knownAs: new FormControl(''),
        address: new FormControl({
            street: new FormControl(''),
            city: new FormControl(''),
            country: new FormControl('')
        })
    });

    powerForArray: FormArray;
    constructor(private fb: FormBuilder) {
        this.powerFormArray =
            this.foodForm.get("entry") as FormArray;


        /**
         * Built-in validators
         *  */
        
        // Static MIN/Max - required the value of the control to be equal to or great tha nteh given number
        form = this.fb.group({
            name: [10, [Validators.min(4)]]
        });
        console.log(this.foodForm.status);
        static max(max: number);
        from = this.fb.group({
            name: [3, [Validators.max(4)]]
        });

        // Static Required  controls must hnot have anon-empty value
        console.log(this.form.status);
        form=this.fb.group({
            name: ['test value', [Validators.required]]
        });
        console.log(this.form.status);

        //  Static requiredTrue = controls must have a value of true.
        form = this.fb.group({
            name: [true, [Validators.requiredTrue]]
        });
        console.log(this.form.status);

        // static min length - used for arrays and strings, this requires that the lenght of the value should be equal to or less than the given number
        form = this.fb.group({
            name: ['test', [Validators.minLength (4)]]
        });
        console.log(this.form.status)

        // static max lenght - used for arrays and strings this requires that the length of the value should be equal to or less than the given number
        form = this.fb.group({
            name: ['test', [Validators.maxLength (4)]]
        });
        console.log(this.form.status)
    }

    ngOnInit() {
        foodForm = new FormGroup({
            // reference from food component
            
            foodForm = new FormGroup({
                firstName: [''],
                lastName: [''],
                knownAs: [''],
                address: this.fb.group({
                    street: [''],
                    city: [''],
                    country: [''],
                })
            });
            
            entry: new FormArray([])
        })
    }
    // now a method to add and remove an instance of a form control in the form array
    addOrder() {
        (this.form.get("order") as FormArray).push(new FormControl());
    }
    deleteOrder(i: number) {
        (this.form.get("order") as FormArray).removeAt(i);
    }
}