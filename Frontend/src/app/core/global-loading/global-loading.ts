import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-global-loading',
  imports: [MatProgressSpinnerModule],
  templateUrl: './global-loading.html',
  styleUrl: './global-loading.scss',
})
export class GlobalLoading {
  constructor(public dialogRef: MatDialogRef<GlobalLoading>) {}
}
