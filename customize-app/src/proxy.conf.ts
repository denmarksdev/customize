import { environment } from "./environments/enviroment";

const defaultTarget = environment.baseurl;
module.exports = [
    {
        context: ['/v1/**'],
        target: defaultTarget,
        changeOrigin: true,
    }
];