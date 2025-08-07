import { debounceTime, ReplaySubject } from 'rxjs';

export class BaseFilter {
  protected _term: string = '';

  get term(): string {
    return this._term;
  }

  set term(value: string) {
    this._term = value;
    this.$filterChanged.next(this);
  }

  private $filterChanged: ReplaySubject<BaseFilter> =
    new ReplaySubject<BaseFilter>(1);

  updateFilter(filter: any) {
    this.$filterChanged.next(filter);
  }

  get filterChanged() {
    return this.$filterChanged.pipe(debounceTime(500));
  }
}
