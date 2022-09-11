import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LikeParams } from '../_models/likeParams';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User;
  userParams: UserParams;
  likeParams: LikeParams;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
      this.likeParams = new LikeParams();
    })
   }

   getUserParams() {
    return this.userParams;
   }

   setUserParams(params: UserParams) {
    this.userParams = params;
   }

   resetUserParams(){
    this.userParams = new UserParams(this.user);
    return this.userParams;
   }

   getLikeParams() {
    return this.likeParams;
   }

   setLikeParams(params: LikeParams) {
    this.likeParams = params;
   }

  getMembers(userParams: UserParams) {
    var response = this.memberCache.get(Object.values(userParams).join('-'));

    if(response) {
      return of(response);
    }

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http).pipe(map(response => {
      this.memberCache.set(Object.values(userParams).join('-'), response);
      return response;
    }))
  }

  getMember(userName: string) {
    const member = [...this.memberCache.values()].reduce((arr, elem) => arr.concat(elem.result), []).find((member: Member) => member.userName === userName);

    if(member) {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + 'users/' + userName);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  addLike(userName: String) {
    return this.http.post(this.baseUrl + 'likes/' + userName, {});
  }

  getLikes(likeParams: LikeParams) {
    let params = getPaginationHeaders(likeParams.pageNumber, likeParams.pageSize);
    params = params.append('predicate', likeParams.predicate);
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl + 'likes', params, this.http);
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId, {});
  }
}
