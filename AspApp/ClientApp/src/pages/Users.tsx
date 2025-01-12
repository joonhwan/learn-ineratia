import {Head, Link, router} from "@inertiajs/react";
import {useState} from "react";
import Layout from "@/components/shared/Layout.tsx";

export interface UsersModel {
    users: Array<{ id: number, name: string, email: string }>
    search: string
}

export default function UsersPage(props: UsersModel) {
    //const page = usePage();
    const [search, setSearch] = useState(props.search);
    
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
                    {props.users.map((user, _) => (
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
            </div>
        </>
    );
}

//UsersPage.layout = (page: React.ReactNode) => <Layout children={page} />;
