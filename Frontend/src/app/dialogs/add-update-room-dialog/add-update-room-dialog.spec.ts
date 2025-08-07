import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateRoomDialog } from './add-update-room-dialog';

describe('AddUpdateRoomDialog', () => {
  let component: AddUpdateRoomDialog;
  let fixture: ComponentFixture<AddUpdateRoomDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddUpdateRoomDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdateRoomDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
