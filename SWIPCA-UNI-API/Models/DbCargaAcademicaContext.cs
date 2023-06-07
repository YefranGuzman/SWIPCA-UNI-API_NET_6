using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWIPCA_UNI_API.Models;

public partial class DbCargaAcademicaContext : DbContext
{
    public DbCargaAcademicaContext()
    {
    }

    public DbCargaAcademicaContext(DbContextOptions<DbCargaAcademicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AulaLaboratorio> AulaLaboratorios { get; set; }

    public virtual DbSet<CargaAcademica> CargaAcademicas { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Duraccion> Duraccions { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Pensum> Pensums { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TempTabla> TempTablas { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0LOPLPE0;Database=DB_Carga_Academica;User Id=Administrador;Password=123;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__Area__750ECEA499ECB82B");

            entity.ToTable("Area");

            entity.Property(e => e.IdArea).HasColumnName("idArea");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Diciplina)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("diciplina");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignatura).HasName("PK__Asignatu__DD25121498F113D5");

            entity.ToTable("Asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.Credito).HasColumnName("credito");
            entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");
            entity.Property(e => e.IdArea).HasColumnName("idArea");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_area_asignatura");
        });

        modelBuilder.Entity<AulaLaboratorio>(entity =>
        {
            entity.HasKey(e => e.IdAuLa).HasName("PK__Aula_Lab__D866F0E385490F57");

            entity.ToTable("Aula_Laboratorio");

            entity.Property(e => e.IdAuLa).HasColumnName("idAuLa");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.AulaLaboratorios)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_aula");
        });

        modelBuilder.Entity<CargaAcademica>(entity =>
        {
            entity.HasKey(e => e.IdCaHo).HasName("PK__CargaAca__3B7A0699D3E38437");

            entity.ToTable("CargaAcademica");

            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
            entity.Property(e => e.IdJefe).HasColumnName("idJefe");
            entity.Property(e => e.Observacion)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.CargaAcademicas)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CargaAcademica_Carrera");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.CargaAcademicas)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CargaAcademica_Clase");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.CargaAcademicas)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CargaAcademica_Docente");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.CargaAcademicas)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CargaAcademica_Grupo");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera).HasName("PK__Carrera__7B19E791B2683002");

            entity.ToTable("Carrera");

            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.IdDuraccion).HasColumnName("idDuraccion");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdDuraccionNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdDuraccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_duraccion_carrera");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_carrera");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PK__Clase__17317A68041E1C0C");

            entity.ToTable("Clase");

            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.Dia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dia");
            entity.Property(e => e.Docente).HasColumnName("docente");
            entity.Property(e => e.HoraFinal).HasColumnName("horaFinal");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asingnatura_clase");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Clases)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docente_clase");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__91431FE1585BDF09");

            entity.ToTable("Contrato");

            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.HorasLaboral).HasColumnName("horasLaboral");
            entity.Property(e => e.Jornada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("jornada");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__C225F98D74F8982D");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Jefe).HasColumnName("jefe");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_departamento");

            entity.HasOne(d => d.JefeNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.Jefe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jefe_departamento");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.IdDisponibilidad).HasName("PK__Disponib__96A3EB6A8D2F54EC");

            entity.ToTable("Disponibilidad");

            entity.Property(e => e.IdDisponibilidad).HasColumnName("idDisponibilidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Evidencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("evidencia");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.Observacíon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoJustificación).HasColumnName("tipoJustificación");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docente_disponibilidad");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__Docente__595F5B9C00972DCA");

            entity.ToTable("Docente");

            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.Derpartamento).HasColumnName("derpartamento");
            entity.Property(e => e.Disponibilidad).HasColumnName("disponibilidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.TipoContrato).HasColumnName("tipoContrato");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_departamento_docente");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Docente_Usuario1");

            entity.HasOne(d => d.TipoContratoNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.TipoContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_docente");
        });

        modelBuilder.Entity<Duraccion>(entity =>
        {
            entity.HasKey(e => e.IdDuraccion).HasName("PK__Duraccio__B2DFDF1F8A243C1B");

            entity.ToTable("Duraccion");

            entity.Property(e => e.IdDuraccion).HasColumnName("idDuraccion");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Periodo).HasColumnName("periodo");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.IdFacultad).HasName("PK__Facultad__B57E5B20A4454042");

            entity.ToTable("Facultad");

            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("telefono");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ubicacion");
            entity.Property(e => e.Vice).HasColumnName("vice");

            entity.HasOne(d => d.ViceNavigation).WithMany(p => p.Facultads)
                .HasForeignKey(d => d.Vice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_usuario");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__EC597A87ED1D2DD8");

            entity.ToTable("Grupo");

            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.Nombre)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Turno).HasColumnName("turno");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_carrera_grupo");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_turno_grupo");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__4712FB33EADD85CB");

            entity.ToTable("Historial");

            entity.Property(e => e.IdHistorial).HasColumnName("idHistorial");
            entity.Property(e => e.Asignatura).HasColumnName("asignatura");
            entity.Property(e => e.Carrera).HasColumnName("carrera");
            entity.Property(e => e.Docente).HasColumnName("docente");
            entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asingnatura_historial");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_carrera_historial");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Historials)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docente_historial");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario__DE60F33AAC94EAF5");

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_carrera_horario");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clase_horario");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_grupo_horario");
        });

        modelBuilder.Entity<Pensum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Pensum");

            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdDuraccion).HasColumnName("idDuraccion");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany()
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asingnatura_pensum");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany()
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_carrera_pensum");

            entity.HasOne(d => d.IdDuraccionNavigation).WithMany()
                .HasForeignKey(d => d.IdDuraccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Duraccion_pensum");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F7681B860AD");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Titulo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<TempTabla>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TempTabla");

            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdCaHo)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCaHo");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
            entity.Property(e => e.IdJefe).HasColumnName("idJefe");
            entity.Property(e => e.Observacion)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Titulo");

            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany()
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docente_titulo");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__Turno__AA068B01C837EBEF");

            entity.ToTable("Turno");

            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.Dia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dia");
            entity.Property(e => e.HoraFinal).HasColumnName("horaFinal");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9762E1862B");

            entity.ToTable("Usuario");

            entity.Property(e => e.Celular)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("celular");
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(256);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Nick)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nick");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("primerNombre");
            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.SecurityStamp).HasMaxLength(256);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("segundoNombre");
            entity.Property(e => e.TipoRol).HasColumnName("tipoRol");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.TipoRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
