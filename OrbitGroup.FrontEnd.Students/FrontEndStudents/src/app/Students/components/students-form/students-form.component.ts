import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Student } from '../../models/Student';
import { StudentConstants } from '../../models/StudentConstants';
import { StudentsService } from '../../services/students-service.service';
import { StudentsValidator } from '../../validators/StudentsValidator';

@Component({
  selector: 'app-students-form',
  templateUrl: './students-form.component.html',
  styleUrls: ['./students-form.component.scss']
})
export class StudentsFormComponent implements OnInit {

 
  studentForm: FormGroup;
  modify: boolean = false;
  ageArray: Array<number>;

  constructor(
    public dialogRef: MatDialogRef<StudentsFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Student,
    public tudentsService: StudentsService
  ) { }

  ngOnInit() {

    this.modify = !this.isNullOrUndefined(this.data);
    //console.log(this.modify);
    //console.log(this.data);
    this.createAgeList();
    this.initForms();
  }

  createAgeList(){
    this.ageArray = new Array<number>();
    for (let index = StudentConstants.AgeMinValue; index <= StudentConstants.AgeMaxValue; index++) {
      this.ageArray.push(index);      
    }
  }

  isNullOrUndefined(value : any){
    return value === null || value === undefined;
  }

  initForms() {
    this.studentForm = new FormGroup({
      username: new FormControl('', StudentsValidator.usernameValidator()),
      firstName: new FormControl('', StudentsValidator.firstNameValidator()),
      lastName: new FormControl('', StudentsValidator.lastNameValidator()),
      age: new FormControl(0, StudentsValidator.ageValidator()),
      career: new FormControl('', StudentsValidator.careerValidator())
    });
    //console.log(this.data);
    if (this.modify) {
      this.studentForm.get("username").setValue(this.data.username);
      this.studentForm.get("firstName").setValue(this.data.firstName);
      this.studentForm.get("lastName").setValue(this.data.lastName);
      this.studentForm.get("age").setValue(this.data.age);
      this.studentForm.get("career").setValue(this.data.career);
      this.modify = this.data.username != "";
    }
  }

  getErrorMessage(control: string) {
    switch (control) {
      case 'username':
        return this.studentForm.get(control).hasError('required') ? 'User name required' : '';
      case 'firstName':
        return this.studentForm.get(control).hasError('required') ? 'First name required' : '';
      case 'lastName':
        return this.studentForm.get(control).hasError('required') ? 'Last name required' : '';
      case 'age':
        return this.studentForm.get(control).hasError('required') ? 'Age required' : '';
      case 'career':
        return this.studentForm.get(control).hasError('required') ? 'Career required' : '';
      default:
        return '';
    }
  }

  onSubmit(formDirective: FormGroupDirective) {
    let student: Student = new Student();
    student.username = this.studentForm.get("username").value;
    student.firstName = this.studentForm.get("firstName").value;
    student.lastName = this.studentForm.get("lastName").value;
    student.age = this.studentForm.get("age").value;
    student.career = this.studentForm.get("career").value;
    //console.log(item);
    this.dialogRef.close(student);
  }

  //Access to the static contants (done for the HTML access)
  getUsernameMaxLength(){
    return StudentConstants.UsernameMaxLength;
  }
  getFirstNameMaxLength(){
    return StudentConstants.FirstNameMaxLength;
  }
  getLastNameMaxLength(){
    return StudentConstants.LastNameMaxLength;
  }
  getCareerMaxLength(){
    return StudentConstants.CareerMaxLength;
  }
}

