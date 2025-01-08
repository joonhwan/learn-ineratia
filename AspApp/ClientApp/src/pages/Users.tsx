import {Head, usePage} from "@inertiajs/react";
import React from "react";
import Layout from "@/components/shared/Layout.tsx";

export interface UsersModel
{
}

export default function UsersPage(model: UsersModel) {
    //const page = usePage();
    return (
        <>
            <Head title="사용자정보"/>
            <h1 className="">사용자정보</h1>
        </>
    );
}

UsersPage.layout = (page: React.ReactNode) => <Layout children={page} />;
