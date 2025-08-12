import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateBookingDialog } from './add-update-booking-dialog';

describe('AddUpdateBookingDialog', () => {
  let component: AddUpdateBookingDialog;
  let fixture: ComponentFixture<AddUpdateBookingDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddUpdateBookingDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdateBookingDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
