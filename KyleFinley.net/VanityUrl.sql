select *
from VanityUrl

insert into VanityUrl(url, entityid, entitytype)
values('test', newid(), 1)


select NEWID()
