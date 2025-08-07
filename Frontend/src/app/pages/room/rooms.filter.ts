import { BaseFilter } from '../../core/base.filter';

export class RoomsFilter extends BaseFilter {
  constructor() {
    super();
  }

  private _type: number = -1;
  private _status: number = -1;
  private _capacity: number = -1;

  get type(): number {
    return this._type;
  }

  set type(value: number) {
    console.log(value);
    this._type = value;
    this.updateFilter(this);
  }

  get status(): number {
    return this._status;
  }

  set status(value: number) {
    console.log(value);
    this._status = value;
    this.updateFilter(this);
  }

  get capacity(): number {
    return this._capacity;
  }

  set capacity(value: number) {
    console.log(value);
    this._capacity = value;
    this.updateFilter(this);
  }
}
