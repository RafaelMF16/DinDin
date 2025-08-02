import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../../components/error-dialog/error-dialog.component';
import { ProblemDetailsError } from '../../interfaces/problemDetailsError.interface';

@Injectable({
  providedIn: 'root'
})
export class ErrorModalService {
  private dialogRef: MatDialogRef<ErrorDialogComponent> | null = null;
  private isShowingModal = false;

  constructor(private dialog: MatDialog) {}

  show(error: ProblemDetailsError): void {
    if (this.isShowingModal || this.dialog.openDialogs.length > 0) {
      return;
    }

    this.isShowingModal = true;
    this.dialogRef = this.dialog.open(ErrorDialogComponent, {
      width: '400px',
      data: error,
      disableClose: true
    });

    this.dialogRef.afterClosed().subscribe(() => {
      this.isShowingModal = false;
      this.dialogRef = null;
    });
  }

  isModalShown(): boolean {
    return this.isShowingModal || this.dialog.openDialogs.length > 0;
  }
}