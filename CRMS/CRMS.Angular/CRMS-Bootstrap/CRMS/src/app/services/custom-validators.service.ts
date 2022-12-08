import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl, ValidationErrors, FormGroup, FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService {

  constructor() { }

  public compareValidator(controlToValidate:string , controlToCompare:string) : ValidatorFn {

     return (formGroup : AbstractControl) : ValidationErrors | null => {
      if (!(formGroup.get(controlToValidate) as FormControl).value)
      return null; //return, if the confirm password is null
      
      if((formGroup.get(controlToValidate) as FormControl).value == (formGroup.get(controlToCompare) as any).value) {
        return null;
      } else {
        (formGroup.get(controlToValidate) as FormControl).setErrors({compareValidator:{valid:false}});
        return {compareValidator:{valid:false}}; 
      }
      
    };

  }
}
