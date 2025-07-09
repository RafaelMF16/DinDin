import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../../components/error-dialog/error-dialog.component';
import { ProblemDetailsError } from '../../interfaces/problemDetailsError.interface';

@Injectable({
  providedIn: 'root'
})
export class ErrorModalService {

  constructor(private dialog: MatDialog) { }

  show(error: ProblemDetailsError): void {
    this.dialog.open(ErrorDialogComponent, {
      width: '400px',
      data: error
    });
  }
}
