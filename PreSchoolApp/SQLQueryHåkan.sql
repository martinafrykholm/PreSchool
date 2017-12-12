select
	*
from
	prs.C2P

select
	C.*,
	' - - - ',
	S.*
from
	prs.Children as C
inner join
	prs.Schedules as S on
	C.Id = S.ChildrenID

select
	*
from
	prs.Schedules