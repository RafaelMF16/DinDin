import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Errors } from '../../interfaces/errors.interface';

@Component({
  selector: 'app-error-dialog',
  standalone: false,
  templateUrl: './error-dialog.component.html',
  styleUrl: './error-dialog.component.css'
})
export class ErrorDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ErrorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Errors
  ) { }

  objectKeys = Object.keys;

  getFirstError(errors: Record<string, string[]>): string | null {
    const firstKey = Object.keys(errors)[0];
    return firstKey ? errors[firstKey][0] : null;
  }
}
