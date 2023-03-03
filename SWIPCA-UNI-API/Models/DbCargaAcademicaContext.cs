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

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<ContratoVigente> ContratoVigentes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Disciplina> Disciplinas { get; set; }

    public virtual DbSet<DisciplinaAsignatura> DisciplinaAsignaturas { get; set; }

    public virtual DbSet<DisciplinaDocente> DisciplinaDocentes { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Laboratorio> Laboratorios { get; set; }

    public virtual DbSet<Pensum> Pensums { get; set; }

    public virtual DbSet<Periodo> Periodos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0LOPLPE0;Database=DB_Carga_Academica;User ID=Administrador;Password=123;Trusted_Connection=True; Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignatura).HasName("PK__Asignatu__DD251214AAE52DD6");

            entity.ToTable("Asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Departamento_asignatura");
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.IdAula).HasName("PK__Aula__D861CCCB3ACD3658");

            entity.ToTable("Aula");

            entity.Property(e => e.IdAula).HasColumnName("idAula");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroAula)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Aulas)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_aula");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera).HasName("PK__Carrera__7B19E7913F91D8A7");

            entity.ToTable("Carrera");

            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_carrera");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClase).HasName("PK__Clase__17317A689F456EA2");

            entity.ToTable("Clase");

            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.Dia)
                .HasMaxLength(10)
                .HasColumnName("dia");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.HoraFinal).HasColumnName("horaFinal");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

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
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__91431FE1D846A944");

            entity.ToTable("Contrato");

            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.HorasLaboral).HasColumnName("horasLaboral");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_contrato");
        });

        modelBuilder.Entity<ContratoVigente>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PK__Contrato__91431FE164E593F6");

            entity.ToTable("ContratoVigente");

            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Jornada)
                .HasMaxLength(10)
                .HasColumnName("jornada");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__C225F98D2D0D1FFE");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_departamento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jefe_departamento");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.HasKey(e => e.IdDisciplina).HasName("PK__Discipli__928F50EFD28717C7");

            entity.ToTable("Disciplina");

            entity.Property(e => e.IdDisciplina).HasColumnName("idDisciplina");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdTitulo).HasColumnName("idTitulo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_departamento_nivel");

            entity.HasOne(d => d.IdTituloNavigation).WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.IdTitulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_titulo_nivel");
        });

        modelBuilder.Entity<DisciplinaAsignatura>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Disciplina_Asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdDisciplina).HasColumnName("idDisciplina");
        });

        modelBuilder.Entity<DisciplinaDocente>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Disciplina_Docente");

            entity.Property(e => e.IdDisciplina).HasColumnName("idDisciplina");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.IdDisponibilidad).HasName("PK__Disponib__96A3EB6A94C244F4");

            entity.ToTable("Disponibilidad");

            entity.Property(e => e.IdDisponibilidad).HasColumnName("idDisponibilidad");
            entity.Property(e => e.Dia)
                .HasMaxLength(10)
                .HasColumnName("dia");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.IdPeriodo).HasColumnName("idPeriodo");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docente_disponibilidad");

            entity.HasOne(d => d.IdPeriodoNavigation).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.IdPeriodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_periodo_disponibilidad");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__Docente__595F5B9CD5072E95");

            entity.ToTable("Docente");

            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_docente");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_departamento_docente");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.IdFacultad).HasName("PK__Facultad__B57E5B20CC046DAD");

            entity.ToTable("Facultad");

            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Extension).HasColumnName("extension");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Recinto)
                .HasMaxLength(75)
                .HasColumnName("recinto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facultads)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vice_facultad");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__EC597A87C89839EA");

            entity.ToTable("Grupo");

            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.Nombre)
                .HasMaxLength(5)
                .HasColumnName("nombre");

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
            entity.HasKey(e => e.IdHistorial).HasName("PK__Historia__4712FB3388053958");

            entity.ToTable("Historial");

            entity.Property(e => e.IdHistorial).HasColumnName("idHistorial");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdDocente).HasColumnName("idDocente");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

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
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario__DE60F33A3D6B4386");

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");
            entity.Property(e => e.IdClase).HasColumnName("idClase");
            entity.Property(e => e.IdGrupo).HasColumnName("idGrupo");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

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

        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorio).HasName("PK__Laborato__BB1322B69C8FD8BC");

            entity.ToTable("Laboratorio");

            entity.Property(e => e.IdLaboratorio).HasColumnName("idLaboratorio");
            entity.Property(e => e.IdFacultad).HasColumnName("idFacultad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Laboratorios)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facultad_laboratorio");
        });

        modelBuilder.Entity<Pensum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pensum");

            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany()
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asingnatura_pensum");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany()
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_carrera_pensum");
        });

        modelBuilder.Entity<Periodo>(entity =>
        {
            entity.HasKey(e => e.IdPeriodo).HasName("PK__Periodo__90A7D3D8845B2DC4");

            entity.ToTable("Periodo");

            entity.Property(e => e.IdPeriodo).HasColumnName("idPeriodo");
            entity.Property(e => e.HoraFinal).HasColumnName("horaFinal");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Periodos)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_turno_periodo");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F7613CCAA14");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.Nombrerol)
                .HasMaxLength(30)
                .HasColumnName("nombrerol");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity.HasKey(e => e.IdTitulo).HasName("PK__Titulo__A3113E5706781BAA");

            entity.ToTable("Titulo");

            entity.Property(e => e.IdTitulo).HasColumnName("idTitulo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__Turno__AA068B01AC6DB895");

            entity.ToTable("Turno");

            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6BB9F6FDE");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Celular)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("celular");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(32)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("date");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(40)
                .HasColumnName("primerApellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(40)
                .HasColumnName("primerNombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(40)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(40)
                .HasColumnName("segundoNombre");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(10)
                .HasColumnName("usuario");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
