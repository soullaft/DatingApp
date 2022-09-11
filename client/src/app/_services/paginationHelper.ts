import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs";
import { PaginatedResult } from "../_models/pagination";

export function getPaginatedResult<T>(url, params, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(respose => {
        paginatedResult.result = respose.body;

        if (respose.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(respose.headers.get('Pagination'));
        }

        return paginatedResult;
      })
    );
  }

  export function getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }