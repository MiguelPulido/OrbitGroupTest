import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BarService } from 'src/app/Utils/messages/BarService/bar.service';
import { DialogMessageService } from 'src/app/Utils/messages/DialogMessageService/dialog-message.service';
import { Student } from '../../models/Student';
import { StudentsService } from '../../services/students-service.service';
import { StudentsFormComponent } from '../students-form/students-form.component';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.scss']
})
export class StudentsListComponent implements OnInit {


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = ['username', 'firstName', 'lastName', 'age', 'career', 'edit', 'delete'];//columns to display
  dataSource = new MatTableDataSource(new Array<Student>());

  showLoadingSpinner: boolean = false;

  private dialogAddElementRef;

  constructor(
    public dialog: MatDialog,
    private dialogMessages: DialogMessageService,
    private _studentsService:StudentsService,
    private messageBar : BarService
  ) { }

  ngOnInit() {
    this.dataSource = new MatTableDataSource(new Array<Student>());
    this.refreshTable();
    this.searchStudents();
  }
  
  refreshTable() {
    this.paginator = this.paginator;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  isNullOrUndefined(value : any){
    return value === null || value === undefined;
  }

  async deleteTableElement(item: Student) {

    let confirmDeletion = await this.dialogMessages.openDialogConfirm(
      '400px',
      "Are you sure to delete the user: <b>" + item.username + "</b>?"
    );

    if (confirmDeletion) {
      this.deleteStudent(item);
    }
  }

  modifyTableElement(item: Student) {
    this.openDialogFormNewWorkItem(item);
  }  

  openDialogFormNewWorkItem(studentToModify: Student): void {

    this.dialogAddElementRef = this.dialog.open(StudentsFormComponent, {
      width: '400px',
      data: studentToModify
    });

    this.dialogAddElementRef.afterClosed().subscribe(studentData => {
      if(this.isNullOrUndefined(studentData)){
        return;
      }
      if(this.isNullOrUndefined(studentToModify)){
        this.createStudent(studentData);
        return;
      }
      studentData.id=studentToModify.id;
      this.updateStudent(studentData);
    });

  }
  searchStudents(){
    this.showLoadingSpinner=true;
    this._studentsService.getStudents().subscribe(
      result =>{
        this.dataSource.data = result as Student[];
        //console.log(result);
        this.showLoadingSpinner=false;        
        this.refreshTable();
      },
      error=>{
        this.showLoadingSpinner=false;
        this.messageBar.openSnackBar(error,'Ok');
      }
    );
  }

  deleteStudent(student:Student){
    this.showLoadingSpinner=true;
      this._studentsService.deleteStudent(student.id).subscribe(
        result =>{
          //console.log(result);
          this.showLoadingSpinner=false;
          this.messageBar.openSnackBar('Student deleted, username: ' + student.username,'Ok');
          this.searchStudents();
        },
        error=>{
          this.showLoadingSpinner=false;
          this.messageBar.openSnackBar(error,'Ok');
        }
      );
  }
  createStudent(student:Student){
    this.showLoadingSpinner=true;
    this._studentsService.createStudent(student).subscribe(
      created =>{
        //console.log(created);
        this.showLoadingSpinner=false;
        this.messageBar.openSnackBar('Student created, username: ' + student.username,'Ok');
        this.searchStudents();
      },
      error=>{
        this.showLoadingSpinner=false;
        this.messageBar.openSnackBar(error,'Ok');
      }
    );
  }
  updateStudent(student:Student){
    this.showLoadingSpinner=true;
    this._studentsService.updateStudent(student).subscribe(
      updated =>{
        //console.log(updated);
        this.showLoadingSpinner=false;
        this.messageBar.openSnackBar('Student updated, username: ' + student.username,'Ok');
        this.searchStudents();
      },
      error=>{
        this.showLoadingSpinner=false;
        this.messageBar.openSnackBar(error,'Ok');
      }
    );
  }

}
