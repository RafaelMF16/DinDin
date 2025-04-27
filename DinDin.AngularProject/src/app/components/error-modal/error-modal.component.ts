import { Component, Inject, inject, Input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-error-modal',
  standalone: false,
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.css',
})
export class ErrorModalComponent {
  @Input() error!: any;

  constructor(
    public errorModal: MatDialogRef<ErrorModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {error: string}
  ) {}

  onCancel(): void {
    this.errorModal.close();
  }
}
