import {Head, Link, router} from "@inertiajs/react";
import {useState} from "react";
import {PagedResult} from "@/components/shared/common.ts";

export interface UserDto {
    id: number
    name: string
    email: string
}

export interface UsersModel {
    data: PagedResult<UserDto>
    search: string
};

const renderPaginationLinks = (currentPage: number, totalPages: number) => {

    const maxVisible = 7;  // 최대 보여질 페이지 수

    let pages: (number | string)[] = [];
    const ELLIPSIS = '...';

    if (totalPages <= maxVisible) {
        // 전체 페이지가 maxVisible 이하면 모두 표시
        pages = Array.from({length: totalPages}, (_, i) => i + 1);
    } else {
        // 항상 첫 페이지 포함
        pages.push(1);

        if (currentPage > 3) {
            pages.push(ELLIPSIS);
        }

        // 현재 페이지 주변 페이지들
        for (let i = Math.max(2, currentPage - 1);
             i <= Math.min(totalPages - 1, currentPage + 1);
             i++) {
            pages.push(i);
        }

        if (currentPage < totalPages - 2) {
            pages.push(ELLIPSIS);
        }

        // 항상 마지막 페이지 포함
        if (currentPage < totalPages) {
            pages.push(totalPages);
        }
    }

    return pages.map((page, index) => {
        if (page === '...') {
            return <span key={`ellipsis-${index}`} className="flex-1 px-3 py-2">...</span>;
        }

        const pageNumber = page as number;
        return (
            <Link
                key={pageNumber}
                href={`/users?page=${pageNumber}`}
                className={`place-items-center px-3 py-2${
                    currentPage === pageNumber
                        ? 'bg-primary text-primary-content'
                        : 'text-blue-500 hover:bg-primary hover:text-primary-content'
                } rounded-md h-full flex justify-center items-center text-center`}
            >
                {pageNumber}
            </Link>
        );
    });
};


export default function UsersPage(props: UsersModel) {
    //const page = usePage();
    const [search, setSearch] = useState(props.search);

    const baseClasses = "px-3 py-2 place-items-center rounded-md h-full flex justify-center items-center text-center";
    const activeClasses = "text-blue-500 hover:bg-primary hover:text-primary-content";
    const disabledClasses = "text-gray-400 cursor-not-allowed opacity-50";

    return (
        <>
            <Head>
                {/*<title> "사용자정보" </title>*/}
                <meta name="description" content="사용자정보 에 대한 정보" head-key={"description"}/>
            </Head>
            <h1 className="mb-4">사용자 목록</h1>

            <div className={"mt-4"}>
                <div>
                    <input type="text" className={"bg-primary text-primary-content px-4 py-2 w-full rounded-md "}
                           placeholder="사용자 이름을 검색하세요"
                           value={props.search}
                           onChange={(e) => {
                               //console.log(e.target.value);
                               router.get(`/users?search=${e.target.value}`, {
                                   search: e.target.value
                               }, {
                                   preserveState: true,
                                   preserveScroll: true,
                                   // preserveMethod: true,
                                   // only: ["search"]
                               })
                           }}
                    />
                </div>
                <div className="overflow-x-auto shadow-md sm:rounded-lg">
                    {props.data.items.map((user, _) => (
                        <div key={user.id} className="px-4 py-2 border-b hover:bg-gray-900 flex justify-between">
                            <div className="">
                                <p className={"font-bold"}>{user.name}</p>
                                <p>{user.email}</p>
                            </div>
                            <div className="min-w-16">
                                <Link href={"/users/" + user.id}
                                      className={"place-items-center m-0 text-blue-500 hover:bg-primary hover:text-primary-content rounded-md h-full flex justify-center items-center text-center"}>편집</Link>
                            </div>
                        </div>
                    ))}
                </div>
                <div className="flex justify-between mx-auto w-[80%] gap-4 items-center mt-4">


                    {props.data.hasPreviousPage ? (
                        <Link
                            href={"/users?page=" + (props.data.pageNumber - 1)}
                            className={`${baseClasses} ${activeClasses}`}
                        >
                            이전 페이지
                        </Link>
                    ) : (
                        <span className={`${baseClasses} ${disabledClasses}`}>
                            이전 페이지
                        </span>
                    )}

                    {
                        renderPaginationLinks(props.data.pageNumber, props.data.totalPages)
                    }


                    {props.data.hasNextPage ? (
                        <Link
                            href={"/users?page=" + (props.data.pageNumber + 1)}
                            className={`${baseClasses} ${activeClasses}`}
                        >
                            다음 페이지
                        </Link>
                    ) : (
                        <span className={`${baseClasses} ${disabledClasses}`}>
                            다음 페이지
                        </span>
                    )}

                </div>
            </div>
        </>
    );
}

//UsersPage.layout = (page: React.ReactNode) => <Layout children={page} />;
