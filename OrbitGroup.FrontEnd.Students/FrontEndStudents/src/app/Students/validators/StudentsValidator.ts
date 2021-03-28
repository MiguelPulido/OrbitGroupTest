import { Validators } from "@angular/forms";
import { StudentConstants } from "../models/StudentConstants";

export class StudentsValidator {
    

    static usernameValidator(){
        return this.stringValidatorRequiredMinAndMaxLength(StudentConstants.UsernameMinLength,StudentConstants.UsernameMaxLength);
    }
    static firstNameValidator(){
        return this.stringValidatorRequiredMinAndMaxLength(StudentConstants.FirstNameMinLength,StudentConstants.FirstNameMaxLength);
    }
    static lastNameValidator(){
        return this.stringValidatorRequiredMinAndMaxLength(StudentConstants.LastNameMinLength,StudentConstants.LastNameMaxLength);
    }
    static careerValidator(){
        return this.stringValidatorRequiredMinAndMaxLength(StudentConstants.CareerMinLength,StudentConstants.CareerMaxLength);
    }
    static ageValidator(){
        return Validators.compose(
            [
                Validators.required,
                Validators.min(StudentConstants.AgeMinValue) 
            ]); 
    }
    static stringValidatorRequiredMinAndMaxLength(minLength:number, maxLength:number){
        return Validators.compose(
            [
                Validators.required,
                Validators.minLength(minLength),
                Validators.maxLength(maxLength)            
            ]);
    }
}