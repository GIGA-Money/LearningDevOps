/* 
This particular component only demos methods, and is never intended to be implemented,
being used as a stoping ground for templates, learning, research, and with the occasional testing.
*/
import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/froms';

@Component({
    selector: 'form',
    templateUrl: './food-form.component.html',
    styleUrls: ['./food-form.component.sass']
})
export class FoodFormComponent {
    userForm = new FormGroup({
        email: new FormControl(),
        firstName: new FormControl(),
        lastNam: new FormControl(),
    });

    // setValue method, sets the new value for the control
    setValue(value: TValue, options?: {
        onlySelf?: boolean;
        emitEvent?: boolean; emitModelToViewChange?: boolean; emitViewToModelChange?: boolean;
    }): void

    // patchValue: patches the value of a control
    patchValue(value: TValue, options?: {
        onlySelf?: boolean;
        emitEvent?: boolean; emitModelToViewChange?: boolean; emitViewToModelChange?: boolean;
    }): void

    // getRawValue: retriview the value of a form control, used on disabled form controls
    getRawValue(): TValue

    // reset: restes the form control form its default and mark the control as pristine and untouched.
    reset(formState?: TValue | FormControlState<TValue>, options?: {
        onlySelf?: boolean;
        emitEvent?: boolean
    }): void

    // registerOnChange(): registers a listener to emit events one the form control value is changed.
    registerOnChange(fn: Function): void
    // Note: function - the method that is called when the value changes.

    // registerOnDisabledChange: register a listener to emit events once the isDisabled status of the control chagnes.
    registerOnDisabledChange(fn: (isDisabled: boolean) => void): void
    // Note: function - the method that is called when the disabled status changes.


    // initialize the form controls. set the value, disabled state, and validators of a specific form.
    const control = new FormControl('Food!'); 
    console.log(control.value); //Food

    // initializing form control with an intial vlaue nad disabled sate:
    const control = new Formcontrol({
        value: 'Food',
        disabled: true
    });
    console.log(control.value, control.status);

    // intializing a form control withh a intial value nd an array of built in validators:
    const control = new FormControl('', Validators.email, Validators.required);
    console.log(control.status);

    /*
    Resetting form controls
    */

    // Restting controls to specific value:
    const control = new FormControl('Texas Cheese Steak');
    console.log(control.value);
    control.reset('Philly Cheese Steak');
    console.log(control.value);

    // Resetting controls to an initial value
    const control = new FormControl('Texas Cheese Steak');
    console.log(control.value);
    control.reset();
    console.log(control.value);

    // Resetting controls with a vlue and a disabled state
    const control = new FormControl('Texas Cheese Steak');
    console.log(control.value);
    console.log(control.status);
    control.reset({ value: 'Philly Cheese Steak', disabled: true});
    console.log(control.value);
    console.log(control.status);

    /*
    Listening to events
    */

    // Listening to value changes:
    control = new FormControl('');
    this.control.statusChanges.subscribe((data) => {
        console.log(data);
    }); control.disable();
}