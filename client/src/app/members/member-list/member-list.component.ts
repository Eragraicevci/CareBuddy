import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/_services/members.service';
import { Pagination } from 'src/app/models/pagination';
import { UserParams } from 'src/app/models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  //members$: Observable<Member[]> | undefined;
  members: Member[]=[];
  pagination: Pagination | undefined;
  userParams: UserParams| undefined;
  genderList=[{value:'male', display:'Males'},{value:'female', display:'Females'}]

  constructor(private memberService: MembersService, private accountService:AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user=>{
        if(user){
          this.userParams=new UserParams(user);
        }
      }
    })
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    if(!this.userParams) return;
      this.memberService.getMembers(this.userParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
        }
      }
      })
    }

    resetFilters() {
      this.userParams = this.memberService.resetUserParams();
      this.loadMembers();
    }  
      


    pageChanged(event: any) {
      if (this.userParams && this.userParams?.pageNumber!==event.page) {
        this.userParams.pageNumber = event.page;
        this.loadMembers();
      }
    }
}