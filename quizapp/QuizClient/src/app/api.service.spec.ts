import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';

import { ApiService } from './api.service';

fdescribe('ApiService', () => {
  let service: ApiService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiService],
    });
    service = TestBed.inject(ApiService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('posting a qusetion returns valid response', () => {
    service.postQuestion('blah').subscribe((res) => {
      expect(res.status).toBe(200);
    });

    const req = httpTestingController.expectOne('/api/post-question');
    expect(req.request.method).toEqual('POST');

    req.flush({ status: 200, message: 'successfully added question' });
  });
});
