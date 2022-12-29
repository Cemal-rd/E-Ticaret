export interface User {
    id: number,
    email: string,
    passwordHash: any,
    passwordSalt: any,
    verificationToken: string,
    verifiedAt: any,
    passwordResetToken: any,
    resetTokenExpires: any
}
