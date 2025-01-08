import {Head} from "@inertiajs/react";
import React from "react";
import Layout from "@/components/shared/Layout.tsx";

export interface SettingsModel {
}

export default function SettingsPage(model: SettingsModel) {
    //const page = usePage();
    return (
        <>
            <Head title="설정"/>
            <h1 className="">설정</h1>
        </>
    );
}

SettingsPage.layout = (page: React.ReactNode) => <Layout children={page} />;
