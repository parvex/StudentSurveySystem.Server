import { ValidatorFn, AbstractControl } from '@angular/forms';

export function arrayIsNotEmptyValidator(): ValidatorFn {
  return (control: AbstractControl): {[key: string]: any} | null => {
    return control?.value.length > 0 ? null : {'isEmpty': {control: control}};
  };
}
