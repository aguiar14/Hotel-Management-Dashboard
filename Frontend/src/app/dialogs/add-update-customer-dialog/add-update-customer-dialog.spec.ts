import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateCustomerDialog } from './add-update-customer-dialog';

describe('AddUpdateCustomerDialog', () => {
  let component: AddUpdateCustomerDialog;
  let fixture: ComponentFixture<AddUpdateCustomerDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddUpdateCustomerDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdateCustomerDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
