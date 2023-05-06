import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/form';

export function checkHasNumberValidator(): ValidatorFn {
    foodForm = new FormGroup({
        firstName: ['', [checkHasNumberValidator]],
        lastName: ['', [checkHasNumberValidator]],
        knownAs: [''],
        address: this.fb.group({
            street: [''],
            city: [''],
            country: [''],
        })
    });
    orders: this.fb.array({});
    return (control: AbstractControl): ValidationErrors |
        null => {
        const error = /\d/.test(control.value);
        return error ? { hasNumbers: { value: control.value } } : null;
    }
}