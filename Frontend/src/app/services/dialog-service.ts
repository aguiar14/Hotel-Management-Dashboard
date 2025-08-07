import { Injectable } from '@angular/core';
import { GlobalLoading } from '../dialogs/global-loading/global-loading';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root',
})
export class DialogService extends MatDialog {
  public showLoading() {
    this.open(GlobalLoading, {
      disableClose: true,
      panelClass: 'loading-dialog',
    });
  }

  public hideLoading() {
    this.closeAll();
  }
}
