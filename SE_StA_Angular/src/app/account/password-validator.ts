import { FormControl, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';

export interface ValidationResult {
  [key: string]: boolean;
}

export class PasswordValidator {
  public static strong(control: FormControl): Observable<ValidationErrors | null> {
    const hasNumber = /\d/.test(control.value);
    const hasUpper = /[A-Z]/.test(control.value);
    const hasLower = /[a-z]/.test(control.value);
    const hasNonAlphanumeric = /[§@$!%*?&]/.test(control.value);
    const hasLength = /.{12,}/.test(control.value);
    const valid = hasNumber && hasUpper && hasLower && hasNonAlphanumeric && hasLength;
    if (!valid) {
      // return what's not valid
      return of({
        hasNumber: !hasNumber,
        hasUpper: !hasUpper,
        hasLower: !hasLower,
        hasNonAlphanumeric: !hasNonAlphanumeric,
        hasLength: !hasLength
      });
    }
    return of(null);
  }
}
