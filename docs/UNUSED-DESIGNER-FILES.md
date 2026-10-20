# ملفات غير مستخدمة داخل المشروع

هذه الملفات ظهرت نتيجة إنشاء Classes كأنها Forms داخل Visual Studio. هي غير مضافة إلى `Managementclinic.vbproj` ولا يعتمد عليها المشروع:

- `BranchBAL.Designer.vb`, `BranchBAL.resx`
- `BranchDAL.Designer.vb`, `BranchDAL.resx`
- `DBconnectionfactory.Designer.vb`, `DBconnectionfactory.resx`
- `doctorBAL.Designer.vb`, `doctorBAL.resx`
- `DoctorDAL.Designer.vb`, `DoctorDAL.resx`
- `userBAL.Designer.vb`, `userBAL.resx`
- `UserDAL.Designer.vb`, `UserDAL.resx`

الأفضل تركها خارج المشروع أو نقلها إلى أرشيف بعد التأكد من البناء، ولا يتم فتحها من Designer.
