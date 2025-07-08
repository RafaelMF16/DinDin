import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose } from '@angular/material/dialog';
import { Errors } from '../../interfaces/errors.interface';
import { MatDivider } from '@angular/material/divider';
import { CdkScrollable } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-error-dialog',
  templateUrl: './error-dialog.component.html',
  styleUrl: './error-dialog.component.css',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDivider,
    CdkScrollable,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose
  ]
})
export class ErrorDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Errors
  ) { }

  getFirstError(errors: Record<string, string[]>): string | null {
    debugger
    const firstKey = Object.keys(errors)[0];
    return firstKey ? errors[firstKey][0] : null;
  }
}
